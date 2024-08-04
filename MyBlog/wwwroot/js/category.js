var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/category/getall'
        },
        "columns": [
            { data: 'categoryId', "width": "5%" },
            { data: 'categoryName', "width": "25%" },
            {
                data: 'categoryId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/admin/category/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                    <a onClick=Delete('/admin/category/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "5%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "你確定要刪除此資料嗎",
        text: "刪除後你將無法回復此資料",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "確定",
        cancelButtonText: "取消"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    console.log(data);
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}