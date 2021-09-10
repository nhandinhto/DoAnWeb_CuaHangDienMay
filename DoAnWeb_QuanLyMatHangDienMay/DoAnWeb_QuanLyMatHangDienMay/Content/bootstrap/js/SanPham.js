﻿//Load data lên table khi document sẵn sàng
$(document).ready(function () {
    
})

//Phương thức load data
function loadSP() {
    $.ajax({
        url: "/Home/ListSP",
        type: "GET",
        contentType: "application/json;charset = utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.MASP + '</td>';
                html += '<td>' + item.TENSP + '</td>';
                html += '<td>' + item.DONGIA + '</td>';
                html += '<td>' + item.MOTA + '</td>';
                html += '<td>' + item.SOLUONG + '</td>';
                html += '<td>' + item.HINH + '</td>';
                html += '<td>' + item.DSHINH + '</td>';
                html += '<td>' + item.GIAMGIA + '</td>';
                html += '<td>' + item.MANSX + '</td>';
                html += '<td>' + item.MATHIETBI + '</td>';
                html += '<td><a href="#" onclick ="return GetbyID(' + item.MASP + ')">Edit</a> |'
                + '<a href="#" onclick ="Delete(' + item.MASP + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}