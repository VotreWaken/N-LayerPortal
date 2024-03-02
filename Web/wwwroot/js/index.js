var openModalBtn1 = document.getElementById('openModalBtn1');
var openModalBtn2 = document.getElementById('openModalBtn2');
var openModalBtn3 = document.getElementById('openModalBtn3');
var modal1 = document.getElementById('myModal1');
var modal2 = document.getElementById('myModal2');
var modal3 = document.getElementById('myModal3');

function openModal(modal, btn) {
    var rect = btn.getBoundingClientRect();
    var leftOffset = 150;
    modal.style.top = rect.bottom + 10 + 'px';
    modal.style.left = rect.left - leftOffset + 'px';
    modal.style.display = 'block';
}

function closeModal(modal) {
    modal.style.display = 'none';
}

// Открытие модального окна при клике на кнопку 1
openModalBtn1.addEventListener('click', function () {
    openModal(modal1, openModalBtn1);
});

// Открытие модального окна при клике на кнопку 2
openModalBtn2.addEventListener('click', function () {
    openModal(modal2, openModalBtn2);
});

// Открытие модального окна при клике на кнопку 3
openModalBtn3.addEventListener('click', function () {
    openModal(modal3, openModalBtn3);
});


// Закрытие модального окна при клике вне его области
window.addEventListener('click', function (event) {
    if (event.target === modal1 || event.target === modal2 || event.target === modal3) {
        closeModal(modal1);
        closeModal(modal2);
        closeModal(modal3);
    }
});

// Закрытие модального окна при скролле
window.addEventListener('scroll', function () {
    closeModal(modal1);
    closeModal(modal2);
});

// Закрытие модального окна при клике в другое место
document.addEventListener('click', function (event) {
    if (!openModalBtn1.contains(event.target) && !modal1.contains(event.target) &&
        !openModalBtn2.contains(event.target) && !modal2.contains(event.target)) {
        closeModal(modal1);
        closeModal(modal2);
    }
});
