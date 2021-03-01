var dataTable;

$(document).ready(function () {
    loadMailsDataTable(id);
});

function loadMailsDataTable(id) {
    dataTable = $('#DT_mail_load').DataTable({
        "ajax": {
            "url": "/mails/GetAll/"+id,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "lp", "width": "20%" },
            { "data": "email", "width": "50%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Mails/Edit/${data}" class='btn btn-success text-white' style='cursor: pointer; width: 70px'>Edit</a>
                            <a  class='btn btn-danger text-white' style='cursor: pointer; width: 70px' onclick=Delete('/Mails/Delete/'+${data})>Delete</a>
                        </div>`
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "searching": false,
        "paging": false,
        "width": "100%"
    })
}

function Delete(url) {
    swal.fire({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icos: "warning",
        buttons: true,
        showCancelButton: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);

                    }
                }
            });
        }
    });
}
