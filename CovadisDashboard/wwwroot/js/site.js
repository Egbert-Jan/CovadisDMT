// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    var max_fields = 10 //maximum input boxes allowed
    var wrapper = $("#items"); //Fields wrapper
    var add_button = $("#addElement"); //Add button ID

    var x = 0; //initlal text box count
    $(add_button).click(function (e) { //on add input button click
        e.preventDefault();
        if (x < max_fields) { //max input box allowed
            x++; //text box increment
            $(wrapper).append('<div class="form-group"><label for="Element' + x + '">Element' + x + '</label>' +
                '<input type="text" class="form-control" name="Element' + x + '" required/>' +
                '<a href="#" class="remove_field"><i class="fa fa-times"></a></div>');
            $("#Elements").val(x);
        }
        else {
            alert('Maximum of ' + max_fields + ' elements allowed');
        }
    });

    $(wrapper).on("click", ".remove_field", function (e) { //user click on remove field
        e.preventDefault(); $(this).parent('div').remove(); x--;
        $("#Elements").val(x);
    });


    //Popupmessage before deleting a config
    //Then redirect back to the overview
    $(".deleteButton").click(function (e) {
        e.preventDefault();
        var id = $(this).attr("id");
        var controller = $(this).attr("name");
        var popup = confirm("Are you sure you want to remove this configuration?!");
        if (popup == true) {
            $.post("/" + controller + "/delete/" + id).done(location.replace("/" + controller))
        }
        else {
            $.post("/" + controller + "/details/" + id)
        }
    });

});
