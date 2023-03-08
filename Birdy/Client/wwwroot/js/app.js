"use strict";

let lock = false;
let header;
const headerId = 'header';
let scroll = false;
let account;

window.debounce = (func, wait, immediate) => {
    let timeout;

    return function executedFunction() {
        const context = this;
        const args = arguments;

        const later = function () {
            timeout = null;
            if (!immediate) func.apply(context, args);
        };

        const callNow = immediate && !timeout;

        clearTimeout(timeout);

        timeout = setTimeout(later, wait);

        if (callNow) func.apply(context, args);
    };
};

const runOnResize = debounce(() => {
    if (document.body.clientWidth > 768 && lock) {
        header.invokeMethodAsync('ToggleHeaderState');
        header.invokeMethodAsync('Refresh');
    }
}, 250);

const runOnScroll = debounce(() => {
    if (document.body.clientWidth > 768) {
        const el = document.getElementById(headerId);
        if (pageYOffset > el.clientHeight) {
            document.querySelector('.on-top').classList.add('visible');
        } else {
            document.querySelector('.on-top').classList.remove('visible');
        }
    }
}, 250);

window.addEventListener('resize', runOnResize);
window.addEventListener('scroll', runOnScroll);

window.setHeader = (dotNetHelper) => {
    header = dotNetHelper;
}

window.scrollOnTop = (id) => {
    document.getElementById(id).scrollIntoView();
}

window.openDialog = (element) => {
    element.showModal();
}

window.closeDialog = (element) => {
    element.close();
}

window.setEditTelFocus = () => {
    document.getElementById('edit_telephone').focus();
}

window.message = (e) => {
    alert(e);
}

window.set = (key, value) => {
    localStorage.setItem(key, value);
}

window.get = (key) => {
    return localStorage.getItem(key);
}

window.remove = (key) => {
    return localStorage.removeItem(key);
}

window.setAccount = (dotNetHelper) => {
    account = dotNetHelper;
}

window.setCartCounter = (value) => {
    account.invokeMethodAsync('SetCounter', value);
}