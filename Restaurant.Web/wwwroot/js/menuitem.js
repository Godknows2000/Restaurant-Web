﻿var dataTable;
$(document).ready(function () {
    dataTable = $('#DT_item').DataTable({
        "ajax": {
            "url": "/api/Menuitem",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "foodType.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="btn-group w-75">
                            <a href="/Admin/MenuItems/upsert?id=${data}" class="btn btn-success mx-2"><i class="fa-solid fa-pen-to-square"></i></a>
                            <a onClick=Delete('/api/MenuItem/'+${data}) class="btn btn-danger mx-2"><i class="fa-solid fa-trash"></i></a>
                            </div>`
                },
                "width": "15%"
            },
        ],
        "width": "100%"
    });
});

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        //success notification
                        toastr.success(data.message);
                    }
                    else {
                        //failure notification
                        toastr.error(data.message)
                    }
                }
            })
        }
    })
}