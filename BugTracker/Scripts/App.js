const answer = document.querySelector('.table');
answer.addEventListener('click', function (event) {
    if (event.target.id === 'Icon') {
        const tbody = event.target.closest('tbody').nextElementSibling;
        if (tbody.style.display === 'none') {
            tbody.style.display = 'table-row-group';
        } else {
            tbody.style.display = 'none';
        }
    }
});