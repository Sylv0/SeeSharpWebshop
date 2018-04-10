// Write your JavaScript code.
const updateCart = (cookie) => {
    fetch(`http://localhost:51421/api/cart/${cookie}`)
        .then(res =>
            res.json())
        .then(data =>
            document.querySelector('span.badge').innerHTML = data);
}
window.onload = () => {
    const cookie = document.cookie.match(/guid=([a-zA-Z0-9-]*)/)[1];
    try {
        document.querySelector("a.text-danger").addEventListener('click', e => {
            if (!window.confirm("Are you sure?")) e.preventDefault();
        });
    } catch (e) {}

    try {
        document.querySelectorAll("input.amount").forEach(e => {
            e.addEventListener("change", e => {
                if (e.target.value <= 0) {
                    if (!window.confirm("You're settng the value to less than 1, would you like to romove it?"))
                        e.target.value = 1;
                    else
                        return;
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
    } catch (e) { console.log(e); }

    try {
        document.querySelectorAll("a.add-to-cart").forEach(e => {
            e.addEventListener('click', e => {
                e.preventDefault();
                let target = e.target;
                if (target.getAttribute('href') == null) target = e.target.parentNode;
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
    } catch (e) {
        console.error(e);
    }
    updateCart(cookie);
}