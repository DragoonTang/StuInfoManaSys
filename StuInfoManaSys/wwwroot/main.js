function DeleteWithController(id, controller) {
    Swal.fire({
        title: "是否确定删除?",
        text: "此操作不可撤销!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "是",
        cancelButtonText: "否",
        animation: false
    }).then(result => {
        if (result.isConfirmed) {
            fetch('/' + controller + '/Delete/' + id)
                .then(response => {
                    if (response.ok) {
                        return response.text().then(text => {
                            Swal.fire({
                                title: text,
                                icon: "success"
                            }).then(() => location.reload());
                        });
                    }
                    else {
                        return response.text().then(text => {
                            Swal.fire({
                                title: "删除失败",
                                text: text,
                                icon: "error"
                            });
                        });
                    }
                });
        }
    });
}