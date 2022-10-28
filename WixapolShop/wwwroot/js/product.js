let dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#productsTable').DataTable({
        "ajax":
        {
            "url": "/admin/product/getall"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "15%" },
            { "data": "producent.name", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "isDiscounted", "width": "5%" },
            { "data": "quantityInStock", "width": "10%" },
            { "data": "retailPrice", "width": "13%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                                <div class="btn-group" role"group">
                                    <a href="/Admin/Product/Edit?id=${data}" class="btn btn-success"><i class="bi bi-pen"></i>Edit</a>
                                    <a  onClick=Delete('/Admin/Product/Delete/${data}') class="btn btn-danger"><i class="bi bi-trash3"></i>Delete</a>

                                </div>
                            `
                },
                "width": "25%"

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