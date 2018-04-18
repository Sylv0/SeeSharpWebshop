// Write your JavaScript code.
const updateCart = (cookie) => {
    fetch(`http://localhost:51421/api/cart/${cookie}`)
        .then(res =>
            res.json())
        .then(data =>
            document.querySelector('span.badge').innerHTML = data);
}

const AddLatestProductToList = (id, n, d, p) => {
    const target = document.querySelector(".row");

    fetch("http://localhost:51421/api/product").then(res => res.json()).then(data => {
        if (data.length > 0) {
            const post = data[data.length - 1];
            console.log(post);
            target.innerHTML += `
                <div class="col-md-4 col-sm-12 product-panel">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">${post.name}</h3>
                        </div>
                        <div class="panel-body">
                            ${post.description}
                        </div>
                        <div class="panel-footer">${post.id} kr<a href="Home/AddItemToCart/${post.id}" class="pull-right add-to-cart"><span class="glyphicon glyphicon-plus"></span></a></div>
                    </div>
                </div>
            `;
        }
    });
}

window.onload = () => {
    const cookie = document.cookie.match(/guid=([a-zA-Z0-9-]*)/)[1];
    try {
        document.querySelector("a.text-danger").addEventListener('click', e => {
            if (!window.confirm("Are you sure?")) e.preventDefault();
        });
    } catch (e) { }

    try {
        document.querySelectorAll("input.amount").forEach(e => {
            e.addEventListener("change", e => {
                if (e.target.value <= 0) {
                    if (!window.confirm("You're settng the value to less than 1, would you like to romove it?"))
                        e.target.value = 1;
                    else {
                        let data = new FormData();
                        data.append('guid', cookie);
                        data.append('Id', e.target.dataset['product']);
                        data.append('Amount', e.target.value);
                        let request = new Request("http://localhost:51421/api/cart/Remove", {
                            method: "POST", body: data
                        });

                        fetch(request)
                            .then(a => a.json())
                            .then(b => {
                                updateCart(cookie);
                            })
                            .catch(e => console.log(e));
                    }
                }

                let data = new FormData();
                data.append('guid', cookie);
                data.append('Id', e.target.dataset['product']);
                data.append('Amount', e.target.value);
                let request = new Request("http://localhost:51421/api/cart/Update", {
                    method: "POST", body: data
                });

                fetch(request)
                    .then(a => a.json())
                    .then(b => {
                        updateCart(cookie);
                    })
                    .catch(e => console.log(e));
            });
        });
    } catch (e) { }

    try {
        document.querySelectorAll("a.add-to-cart").forEach(e => {
            e.addEventListener('click', e => {
                e.preventDefault();
                let target = e.target;
                if (target.getAttribute('href') === null) target = e.target.parentNode;
                let data = new FormData();
                data.append('guid', cookie);
                data.append('Id', target.getAttribute('href').match(/\/([0-9]*$)/)[1]);
                let request = new Request("http://localhost:51421/api/cart/Add", {
                    method: "POST", body: data
                });

                fetch(request)
                    .then(a => a.json())
                    .then(b => {
                        updateCart(cookie);
                    })
                    .catch(e => console.log(e));
            });
        });
    } catch (e) { }

    try {
        document.forms['add_product_form'].addEventListener('submit', e => {
            e.preventDefault();
            let data = new FormData();
            data.append('Name', e.target.name.value);
            data.append('Description', e.target.description.value);
            data.append('Price', e.target.price.value);
            let request = new Request("http://localhost:51421/api/product/Add", {
                method: "POST", body: data
            });

            fetch(request)
                .then(a => a.json())
                .then(b => {
                    if (b === 200) {
                        $('#myModal').modal('hide');
                        AddLatestProductToList();
                    }
                })
                .catch(e => console.log(e));
        });
    } catch (e) { }

    updateCart(cookie);
}