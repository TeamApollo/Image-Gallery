(function () {
   $(document).ready(function () {
       $('#btnUploadFile').on('click', function () {
           var data = new FormData();

           var files = $("#fileUpload").get(0).files;

           if (files.length > 0) {
               data.append("UploadedImage", files[0]);
               console.log(files[0]);
           }

           var ajaxRequest = $.ajax({
               type: "POST",
               url: "http://localhost:55833/api/fileupload",
               contentType: false,
               processData: false,
               data: data,
               // headers: { 'Authorization': 'Bearer ' + localStorage.getItem('access_token') }
           });

           ajaxRequest.done(function (xhr, textStatus) {
               // Do other operation
           });
       });
   });
}());