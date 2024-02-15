// Navbar öğelerini seçin
const navbarItems = document.querySelectorAll('.nav-link');

// Her bir navbar öğesi için click olayını dinleyin
navbarItems.forEach(item => {
    item.addEventListener('click', () => {
        // Tüm navbar öğelerinden "active" sınıfını kaldırın
        navbarItems.forEach(item => {
            item.classList.remove('active');
        });

        // Tıklanan navbar öğesine "active" sınıfını ekleyin
        item.classList.add('active');
    });
});


const body = document.querySelector("body"),
  sidebar = body.querySelector("nav"),
  toggle = body.querySelector(".toggle"),
  searchBtn = body.querySelector(".search-box"),
  modeSwitch = body.querySelector(".toggle-switch"),
  modeText = body.querySelector(".mode-text");
toggle.addEventListener("click", () => {
  sidebar.classList.toggle("close");
});
searchBtn.addEventListener("click", () => {
  sidebar.classList.remove("close");
});
modeSwitch.addEventListener("click", () => {
  body.classList.toggle("dark");
  if (body.classList.contains("dark")) {
    modeText.innerText = "Gündüz";
  } else {
    modeText.innerText = "Gece";
  }
});
