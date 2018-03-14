// Write your JavaScript code.
window.onload = () => {
    document.querySelector("a.text-danger").addEventListener('click', e => {
        if (!window.confirm("Are you sure?")) e.preventDefault();
    });
}