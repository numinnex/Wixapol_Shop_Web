let dataTable;

$(document).ready(function () {
    const url = window.location.search;
    if (url.includes("inprocess")) {
        loadDataTable("processing");
    }
    else {
        if (url.includes("completed")) {
            loadDataTable("completed");
        }
        else {
            if (url.includes("pending")) {
                loadDataTable("pending");
            }
            else {

                if (url.includes("approved")) {
                    loadDataTable("approved");
                }
                else {
                    loadDataTable("all");
                }
            }
        }
    }
});

function loadDataTable(status) {
    dataTable = $('#salesTable').DataTable({
        responsive:
        {
            details:
            {
                type: 'inline',
            }
        },
        columnDefs: [
            { responsivePriority: 1, targets: 0 },
            { responsivePriority: 10004, targets: 4 },
            { responsivePriority: 10003, targets: 5 },
            { responsivePriority: 10002, targets: 3 },
            { responsivePriority: 10001, targets: 2 },
            { responsivePriority: 2, targets: -1 }
        ],
        "ajax":
        {
            "url": "/admin/sale/getall?status=" + status
        },
        "columns": [
            { "data": "orderId", },
            { "data": "subTotal", },
            { "data": "tax", },
            { "data": "total", },
            { "data": "orderStatus", },
            { "data": "paymentStatus", },
            {
                "data": "orderId",
                "render": function (data) {
                    return `
                                <div class="btn-group" role"group">
                                    <a href="/Admin/Sale/Edit?id=${data}" class="btn btn-success"><i class="bi bi-pen"></i>Edit</a>
                                    <a  onClick=Delete('/Admin/Sale/Delete/${data}') class="btn btn-danger"><i class="bi bi-trash3"></i>Delete</a>

                                </div>
                            `
                },
                "width": "15%"


            }
        ]
    });

}

toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-full-width",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "3000",
    "extendedTimeOut": "500",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

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
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }

            })
        }
    })
}