var dataTable;

$(document).ready(function () {
    loadGroupsDataTable();
});

function loadGroupsDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/Groups/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "lp", "width": "20%" },
            {"data": "name", "width": "40%"},
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Groups/Upsert/${data}" class='btn btn-success text-white' style='cursor: pointer; width: 70px'>Edit</a>
                            <a  class='btn btn-danger text-white' style='cursor: pointer; width: 70px' onclick=Delete('/Groups/Delete/'+${data})>Delete</a>
                            <a href="/Mails/Index/${data}" class='btn btn-info text-white' style='cursor: pointer; width: 110px'>E-mails</a>
                        </div>`
                }, "width": "50%"
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
