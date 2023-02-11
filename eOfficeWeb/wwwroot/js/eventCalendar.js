$(document).ready(function () {

    var events = [];
    const URL = "/api/EventCalendar";
    $.ajax(URL)
        .done(function (data) {
            console.log(data.data.length);
            console.log(data.data);
            for (let i = 0; i < data.data.length; i++) {
                events.push({
                    "title": data.data[i]["subject"],
                    description: data.data[i]["description"],
                    start: moment(data.data[i]["start"]),
                    end: data.data[i]["end"] != null ? moment(data.data[i]["end"]) : null,
                    color: data.data[i]["themeColor"],
                    allDay: data.data[i]["isFullDay"]
                });
            }
            GenerateCalender(events);
            //console.log(events);
        })
        .fail(function (error) {
            handleAjaxError(error);
        })
        .always(function () {
            // Anything you want to happen here on either fail or done
            console.log("In the always() method");
        });


    function GenerateCalender(events) {
        $('#calender').fullCalendar('destroy');
        $('#calender').fullCalendar({
            contentHeight: 450,
            defaultDate: new Date(),
            timeFormat: 'h(:mm)a',
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,basicWeek,basicDay,agenda'
            },
            eventLimit: true,
            eventColor: '#378006',
            events: events,
            eventClick: function (calEvent, jsEvent, view) {
                
                $('#myModal #eventTitle').text(calEvent.title);
                var $description = $('<div/>');
                $description.append($('<p/>').html('<b>Start:</b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
                if (calEvent.end != null) {
                    $description.append($('<p/>').html('<b>End:</b>' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
                }
                $description.append($('<p/>').html('<b>Description:</b>' + calEvent.description));
                $('#myModal #pDetails').empty().html($description);
                //$('#myModal').modal();
            }
            
        })
        //console.log("GenerateCalender() ends");
    }  //GenerateCalender() ends


}) //document.ready function ends