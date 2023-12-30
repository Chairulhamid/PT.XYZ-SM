// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var changeColor1 = document.querySelector(".navbar");
var changeColor2 = document.querySelector(' .footer');
var button = document.querySelector(".btn1");
/*const tUbahwarna = document.getElementById('tUbahWarna');
tUbahwarna.onclick = function () {
*//*    changeColor1.style.backgroundColor = 'tomato';*//*
    // document.body.setAttribute('class','biru-muda');
    document.footer.classList.toggle('biru-muda');
};*/

function ubahWarna() {
    changeColor1.style.backgroundColor = '#4b9b49';
    changeColor2.style.backgroundColor = '#4b9b49';
    button.innerHTML = "Reset Color";
    alert("Anda Berhasil Mengubah Warna Thema");
}
button.addEventListener("mouseenter", function () {
    changeColor1.style.backgroundColor = '#405389';
    changeColor2.style.backgroundColor = '#405389';
    button.innerHTML = "Change Color";
})

const tAcakaWarna = document.createElement('button');
const teksButton = document.createTextNode('Acak Warna');

tAcakaWarna.appendChild(teksButton);
tAcakaWarna.setAttribute('type', 'button');
const tUbahwarna = document.getElementById('tUbahWarna');
tUbahwarna.after(tAcakaWarna);

tAcakaWarna.addEventListener('click', function () {
    const r = Math.round(Math.random() * 255 + 1);
    const g = Math.round(Math.random() * 255 + 1);
    const b = Math.round(Math.random() * 255 + 1);
    document.body.style.backgroundColor = 'rgb(' + r + ',' + g + ',' + b + ')';
});