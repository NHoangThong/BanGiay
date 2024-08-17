const listImage = document.querySelector(".img_link");
const imgs = Array.from(document.getElementsByClassName('slide_img'));
const length = imgs.length;
let i = 0;

setInterval(() => {
    if (i == length - 1) {
        i = 0;
        let width = imgs[0].offsetWidth;
        listImage.style.transform = 'translateX(0px)';

    } else {
        i++;
        let width = imgs[0].offsetWidth;
        listImage.style.transform = 'translateX(' + width * -1 * i + 'px)';
    }
}, 3000)