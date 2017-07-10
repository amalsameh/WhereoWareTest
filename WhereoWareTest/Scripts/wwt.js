$(function () {
    var ajaxFormSubmit = function () {
        var $form = $(this);
        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };
        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-otf-target"));
            $target.replaceWith(data);
        });
        return false;
    };
    var getPage = function (e) {
        e.preventDefault();
        var $a = $(this);

        var options = {
            url: $a.attr("href"),
            type:"get"
        };

        $.ajax(options).done(function (data) {
            $("#ProductsList").replaceWith(data);
            $(".pagedList a").click(getPage);
        });
        return false;
    };
    $("#product-image").change(function () {
        imageChange(this)
    });
    $("#btnUpload").on('click', function (e) {
        e.preventDefault();
        $("#product-image").click();
    });
   

   /* $('#upload').fileupload({
        autoUpload: true,
        url: '/Product/UploadImage',
        dataType: 'json',
        add: function (e, data) {
            var jqXHR = data.submit()
                .success(function (data, textStatus, jqXHR) {
                    if (data.isUploaded) {

                    }
                    else {

                    }
                    alert(data.message);
                })
                .error(function (data, textStatus, errorThrown) {
                    if (typeof (data) != 'undefined' || typeof (textStatus) != 'undefined' || typeof (errorThrown) != 'undefined') {
                        alert(textStatus + errorThrown + data);
                    }
                });
        },
        fail: function (event, data) {
            if (data.files[0].error) {
                alert(data.files[0].error);
            }
        }
    });*/
   

    $("form[data-otf-ajax='true']").submit(ajaxFormSubmit);
    //$(".pagedList a").attr("href","javascript:void(0)");
    $(".pagedList a").click(getPage);
    function imageChange(fileControl) {
        console.log(fileControl);
        var file = fileControl.files[0];
        var reader = new FileReader();
        reader.addEventListener("load", function () {
            console.log(reader.result);
            $("#product-image-view").attr("src", reader.result);
            $("#product-image-view").slideDown();
        }, false);

        if (file) {
            reader.readAsDataURL(file);
        }
    }
});
