$(document).ready(function () {
    $('.owl-carousel').owlCarousel({
        loop: true,
        margin: 10,
        autoplay: true,
        autoplayTimeout: 4000,
        items: 1,
        animateOut: 'fadeOut'
    })
});

const navbar = document.getElementById("nav1");
const sticky = navbar.offsetTop;

function myFunction() {
  if (window.scrollY >= sticky) {
  navbar.classList.add("sticky")
  navbar.firstElementChild.firstElementChild.classList.remove("MyNavbar");
  } else {
  navbar.classList.remove("sticky");
  navbar.firstElementChild.firstElementChild.classList.add("MyNavbar");
  }
}