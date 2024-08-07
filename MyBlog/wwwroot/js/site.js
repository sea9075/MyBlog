var body = document.body;

function goAdmin(e) {
    const keyin = [38, 40, 38, 40, 37, 39, 37, 39]
    let keyinIndex = 0;

    document.addEventListener('keydown', function (event) {
        if (event.keyCode === keyin[keyinIndex]) {
            keyinIndex++;
            if (keyinIndex === keyin.length) {
                keyinIndex = 0;
                addAdminLink();
            }
        } else {
            keyinIndex = 0;
        }
    });
    function addAdminLink() {
        var adminlink = document.createElement('a');
        adminlink.href = '/Admin/Post/Index';
        adminlink.textContent = 'Post';
        document.getElementById("goadmin").appendChild(adminlink)
    } 
}
body.addEventListener('keydown', goAdmin, false)