const menuToggle = document.querySelector('.menu-toggle');
const menuNav = document.querySelector('.contenedor-menu-nav');
const menuLinks = document.querySelectorAll('.menu-a');

menuToggle.addEventListener('click', () => {
    menuNav.classList.toggle('active');
    console.log('Toggle menu:', menuNav.classList);
});

menuLinks.forEach(link => {
    link.addEventListener('click', () => {
        menuNav.classList.remove('active');
    });
});

document.addEventListener('click', (event) => {
    const isClickInsideMenu = menuNav.contains(event.target);
    const isClickOnToggle = menuToggle.contains(event.target);

    if (!isClickInsideMenu && !isClickOnToggle) {
        menuNav.classList.remove('active');
    }
});