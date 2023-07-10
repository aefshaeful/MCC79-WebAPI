// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let teks = document.getElementById('teks1');
let button1 = document.getElementById('btn1');
let button2 = document.getElementById('btn2');
let button3 = document.getElementById('btn3');
let button4 = document.getElementById('btn4');
let button5 = document.getElementById('btn5');
let background = document.body;


button1.addEventListener('click', () => {
    teks.style.color = "yellow";
})

button2.addEventListener('click', () => {
    teks.style.backgroundColor = "green";
})

button3.addEventListener("click", (e) => {
    e.preventDefault();
    let randomColor = Math.floor(Math.random() * 16777215).toString(16);
    background.style.backgroundColor = "#" + randomColor;
});

button4.addEventListener('click', () => {
    teks.style.fontSize = '20px';
})

button5.addEventListener('click', () => {
    teks.style.display = 'none';
})
