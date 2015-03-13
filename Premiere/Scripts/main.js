
/*
 * jQuery File Upload Plugin
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2013, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
*/

/*jslint nomen: true, unparam: true, regexp: true */
/*global $, window, document */

$(function () {
    'use strict';

    // In this example we do NOT use the builtin file upload handler.
    // var fileUploadUrl = "/Backload/UploadHandler";
    var title = $('.page-header').html();
    var fileUploadUrl = "Services/UploadHandler.ashx?title="+title;
    var context = $('#fileupload')[0];


    
    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload({
        url: "Services/UploadHandler.ashx?title=" + $('.page-header').html(),
        previewMaxWidth: 80,
        previewMaxHeight: 60,
        acceptFileTypes: /(json)|(txt)|(xml)$/i // Allowed file types
    });
    $('#fileupload').bind('fileuploadadd', function (e, data) {
        //setTimeout(function () { $(".files tr[data-type=image] a").colorbox() }, 1000);
        title = $('.page-header').html();
        fileUploadUrl = "Services/UploadHandler.ashx?title=" + title;
    })

    //Initialize the jQuery file upload progress bar
    $('#fileupload').bind('fileuploadprogress', function (e, data) {
        var progress = parseInt(data.loaded / data.total * 100, 10);
        console.log(progress + '%');
        data.context.find('.progress-bar')
       .css('width',progress + '%')
        //.attr('aria-valuenow', progress)
        //.html(progress)
        //.attr('style',"width:"+progress + "%;")
        //.find('.bar').css(
        //                'width',
        //                progress + '%'
        //);
    })
    //$('#fileupload').bind('fileuploadprogress', function (e, data) {
    //    // Log the current bitrate for this upload:
    //    console.log(data);
    //});


    // Optional: Initial ajax request to load already existing files.
    $.ajax({
        url: fileUploadUrl,
        dataType: 'json',
        context: $('#fileupload')[0]
    })
    .done(function (e, data) {
     
        // Attach the Colorbox plugin to the image files to preview them in a modal window. Other file types (e.g. pdf) will show up in a 
        // new browser window.
        //$(".files tr[data-type=image] a").colorbox();

    });
});

$("document").ready(function () {
    // The Colorbox plugin needs to be informed on new uploaded files in the template in order to bind a handler to it. 
    // There must be a little delay, because the fileuploaddone event is triggered before the new template item is created.
    // A more elegant solution would be to use jQuery's delegated .on method, which automatically binds to the anchors in a
    // newly created template item, and then call colorbox manually.
    $('#fileupload')
        .bind('fileuploadsubmit', function (e, data) {
            //setTimeout(function () { $(".files tr[data-type=image] a").colorbox() }, 1000);
           
        })

        
        // Delete File on server
        .bind('fileuploaddestroy', function (e, data) {
            $.ajax({
                type:"DELETE",
                url: "Services/UploadHandler.ashx?f=" + data.context.find('a').attr('title')+"&title=" + $('.page-header').html(),
                dataType: "xml"
                
            })
            .success(function (e, data) {
                //data.context.remove();
                console.log('done');
            })
            .done(function (e, data) {
                //data.context.remove();
                console.log('done with success');
            })
            .fail(function (e, data) {
                console.log('error!!');
            })
           
            data.context.remove();
            //var s = data.url.split("?");                                // Split and replace is way faster than RegEx here
            //s[1] = s[1].replace("fileName=", "").replace(".", ",");     // Make the url RESTful compliant and remove dots 
            //                                                            // because otherwise IIS treats this as a path to a file
            //                                                            // and it will not be routed by the Web API
            //data.url = s[0] + s[1];                                     // Note: Server side we must add the file manually to the 
            //                                                            // e.Param.BackloadValues.FileName property in the IncomingRequestStarted event handler
        });
        

});