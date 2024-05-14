const allSideMenu = document.querySelectorAll('#sidebar .side-menu.top li a');

allSideMenu.forEach(item => {
    const li = item.parentElement;

    item.addEventListener('click', function () {
        allSideMenu.forEach(i => {
            i.parentElement.classList.remove('active');
        });
        li.classList.add('active');
    });
});

// TOGGLE SIDEBAR
const menuBar = document.querySelector('#content nav .bx.bx-menu');
const sidebar = document.getElementById('sidebar');

// Get reference to the logo image
const logoImage = document.getElementById('logo-collapse')

// Define paths for the expanded and collapsed logos
const expandedLogoPath = '/Dashboard_Images/Main.png'; // Path to the expanded logo
const collapsedLogoPath = '/Dashboard_Images/3.png'; // Path to the collapsed logo

// Define sizes for expanded and collapsed logos
const expandedLogoSize = { width: '200px', height: '200px' }; // Size for expanded logo
const collapsedLogoSize = { width: '40px', height: '40px' }; // Size for collapsed logo

const expandedLogoMargin = '80px'; // Margin for expanded logo
const collapsedLogoMargin = '10px'; // Margin for collapsed logo

menuBar.addEventListener('click', function () {
    sidebar.classList.toggle('hide');
    // Change the logo source and size based on the sidebar state
    if (sidebar.classList.contains('hide')) {
        logoImage.src = collapsedLogoPath;
        logoImage.style.width = collapsedLogoSize.width;
        logoImage.style.height = collapsedLogoSize.height;
        logoImage.style.marginTop = collapsedLogoMargin;
    } else {
        logoImage.src = expandedLogoPath;
        logoImage.style.width = expandedLogoSize.width;
        logoImage.style.height = expandedLogoSize.height;
        logoImage.style.marginTop = expandedLogoMargin;
    }
});

const searchButton = document.querySelector('#content nav form .form-input button');
const searchButtonIcon = document.querySelector('#content nav form .form-input button .bx');
const searchForm = document.querySelector('#content nav form');

searchButton.addEventListener('click', function (e) {
    if (window.innerWidth < 576) {
        e.preventDefault();
        searchForm.classList.toggle('show');
        if (searchForm.classList.contains('show')) {
            searchButtonIcon.classList.replace('bx-search', 'bx-x');
        } else {
            searchButtonIcon.classList.replace('bx-x', 'bx-search');
        }
    }
});

if (window.innerWidth < 768) {
    sidebar.classList.add('hide');
} else if (window.innerWidth > 576) {
    searchButtonIcon.classList.replace('bx-x', 'bx-search');
    searchForm.classList.remove('show');
}

window.addEventListener('resize', function () {
    if (this.innerWidth > 576) {
        searchButtonIcon.classList.replace('bx-x', 'bx-search');
        searchForm.classList.remove('show');
    }
});

const switchMode = document.getElementById('switch-mode');

switchMode.addEventListener('change', function () {
    if (this.checked) {
        document.body.classList.add('dark');
    } else {
        document.body.classList.remove('dark');
    }
});
