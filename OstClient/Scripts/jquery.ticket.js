(function ($) {
	function User(data){
		this.IsAuthenticated = false;
		this.FName = "";
		this.LName = "";
		this.Email = "";
		this.ClientID = 0;
		
		if (data){
			this.IsAuthenticated = true;
			this.FName = data.FName;
			this.LName = data.LName;
			this.Email = data.PrimaryEmail;
			this.ClientID = data.ClientID;
		}
		
		this.getDisplayName = function () {
            return this.FName + ' ' + this.LName;
		};
	}
	
    function Ticket() {
        var self = this;

        this.getUserInfo = function () {
            var def = $.Deferred();

            $.ajax({
                "url": "/webapi/data/client/current",
                "method": "GET",
                "dataType": "json"
            }).done(function (data, textStatus, jqXHR) {
                def.resolve(new User(data));
            }).fail(function (jqXHR, textStatus, errorThrown) {
                def.resolve(new User());
            });

            return def.promise();
        };

        this.getTicketDetail = function (ticketID) {
            var def = $.Deferred();

            self.getUserInfo().done(function (user) {
                if (user.IsAuthenticated) {
                    var name = user.getDisplayName();
                    var email = user.Email;
                    $.ajax({
                        url: "/ostclient/ajax.aspx",
                        method: "POST",
                        dataType: "json",
                        data: { "command": "ticket-detail", "ticketID": ticketID }
                    }).done(function (data, textStatus, jqXHR) {
                        if (!data.error) {
                            data.display_name = name;
                            data.email = email;
                            def.resolve(data);
                        } else {
                            def.reject({ "error": data.message });
                        }
                    }).fail(function (jqXHR, textStatus, errorThrown) {
                        def.reject({ "error": jqXHR.statusText });
                    });
                } else {
                    def.reject({ "error": "You must be logged into LNF Online Services to view this ticket." });
                }
            });

            return def.promise();
        };

        this.postMessage = function (ticketID, message, source) {
            var def = $.Deferred();

            self.getUserInfo().done(function (user) {
                if (user.IsAuthenticated) {
                    var name = user.getDisplayName();
                    var email = user.Email;
                    var s = source ? " from " + source : "";
                    var n = " by " + name;
                    n += email ? " (" + email + ")" : "";
                    message = "Posted" + s + n + "\r\n--------------------------------------------------\r\n" + message;
                    $.ajax({
                        url: "/ostclient/ajax.aspx",
                        method: "POST",
                        dataType: "json",
                        data: { "command": "post-message", "ticketID": ticketID, "message": message },
                        success: function (data) {

                        }
                    }).done(function (data, textStatus, jqXHR) {
                        if (!data.error) {
                            data.display_name = name;
                            data.email = email;
                            def.resolve(data);
                        } else {
                            def.reject({ "error": data.message });
                        }
                    }).fail(function (jqXHR, textStatus, errorThrown) {
                        def.reject({ "error": jqXHR.statusText });
                    });
                } else {
                    def.reject({ "error": "You must be logged into LNF Online Services to post a message." });
                }
            });

            return def.promise();
        };

        this.formatDate = function (date, format) {
            f = format || "MM/DD/YYYY h:mm:ss a";
            return moment(date).format(f);
        };

        this.createLinks = function (text) {
            var exp = /\(?\b(https?|ftp|file):\/\/[-A-Za-z0-9+&@#\/%?=~_()|!:,.;]*[-A-Za-z0-9+&@#\/%=~_()|]/ig;
            var matches = text.match(exp);
            if ($.isArray(matches)) {
                $.each(matches, function (i, m) {
                    if (m.substr(0, 1) === "(" && m.substr(m.length - 1) === ")")
                        m = m.substr(1, m.length - 2);
                    text = text.replace(m, '<a href="' + m + '" target="_blank">' + m + '</a>');
                });
            }
            return text;
        };

        var parseQuery = function () {
            var result = {};
            var search = window.location.search;
            if (search === null || search.length === 0)
                return result;
            if (search.indexOf('?') !== -1)
                search = search.substr(1);
            var splitter = search.split('&');
            $.each(splitter, function (index, value) {
                var pair = value.split('=');
                result[pair[0]] = pair[1];
            });
            return result;
        };

        this.query = parseQuery();
    }

    $.fn.ticket = function (options) {
        return this.each(function () {
            var $this = $(this);

            var opt = $.extend({}, { "source": "", "ticketID": "", "onload": function (ticket) { } }, $this.data(), options);

            var LONG_DATE_FORMAT = "ddd, MMM D YYYY h:mm a";

            var ticket = new Ticket();

            $this.data("source", opt.source);
            $this.data("ticketID", opt.ticketID);

            var loadError = function (err) {
                $(".detail-message", $this).html($("<div/>", { "class": "alert alert-danger", "role": "alert" }).html(err));
            };

            var fill = function (data) {
                if (data.detail.info.ticketID) {
                    //**** info
                    $.each(data.detail.info, function (k, v) {
                        $("[data-property='info." + k + "']", $this).each(function () {
                            var target = $(this);
                            var val = v;
                            if (target.data("dateformat") && v)
                                val = moment(v).format(target.data("dateformat"));

                            if (val) target.html(val);
                        });
                    });

                    //**** thread
                    $(".ticket-thread", $this).each(function () {
                        var thread = $(this);
                        $(".ticket-section", thread).html("");
                        $.each(data.detail.messages, function (m, msg) {
                            $(".ticket-section", thread).append(
                                $("<div/>", { "class": "thread-message" }).append(
                                    $("<div/>", { "class": "thread-message-subject" }).html(moment(msg.created).format("ddd, MMM D YYYY h:mm a"))
                                ).append(
                                    $("<div/>", { "class": "thread-message-body" }).html(ticket.createLinks(msg.message.replace(/\n/g, "<br />")))
                                )
                            );
                            $.each(data.detail.responses, function (r, resp) {
                                if (resp.msg_id === msg.msg_id) {
                                    $(".ticket-section", thread).append(
                                        $("<div/>", { "class": "thread-response" }).append(
                                            $("<div/>", { "class": "thread-response-subject" }).html(moment(resp.created).format("ddd, MMM D YYYY h:mm a") + " - " + resp.staff_name)
                                        ).append(
                                            $("<div/>", { "class": "thread-response-body" }).html(ticket.createLinks(resp.response.replace(/\n/g, "<br />")))
                                        )
                                    );
                                }
                            });
                        });
                    });

                    $(".container-fluid", $this).show();
                    $(".detail-message", $this).html("");
                } else {
                    $(".container-fluid", $this).hide();
                    loadError("Ticket #" + opt.ticketID + " not found");
                }
            };

            var loadTicketDetail = function (ticketID) {
                $(".detail-message", $this).html($("<em/>").css("color", "#808080").html("Loading..."));

                ticket.getTicketDetail(ticketID).done(function (ticket) {
                    fill(ticket);
                }).fail(function (err) {
                    loadError(err.error);
                });
            };

            if (opt.ticketID)
                loadTicketDetail(opt.ticketID);
            else
                loadError('Missing parameter: ticketID');


			
            $this.off("click", ".post-message").on("click", ".post-message", function (e) {
                var btn = $(this);
                $(".new-message-error").html("");
				console.log(opt.ticketID);
				
                // var message = $(".new-message-text", $this).val();
                // if (message === "")
                    // $(".new-message-error", $this).html($("<div/>", { "class": "alert alert-danger", "role": "alert" }).css("margin-top", "10px").html("Please enter a message."));
                // else {
                    // btn.prop("disabled", true);
                    // $('.new-message-working', $this).show();
                    // ticket.postMessage(opt.ticketID, message, opt.source).done(function (ticket) {
                        // $(".new-message-text", $this).val("");
                        // fill(ticket);
                    // }).fail(function (err) {
                        // loadError(err.error);
                    // }).always(function () {
                        // $('.new-message-working', $this).hide();
                        // btn.prop("disabled", false);
                    // });
                // }
            });
        });
    };
}(jQuery));