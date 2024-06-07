function initAjaxElements() {
    document.querySelectorAll('form[data-ajax="true"], a[data-ajax="true"]').forEach(element => {
        if (element.tagName === 'FORM') {
            element.removeEventListener('submit', handleAjaxForm);  // Remove existing event listener to prevent duplicates
            element.addEventListener('submit', handleAjaxForm);
        } else if (element.tagName === 'A') {
            element.removeEventListener('click', handleAjaxLink);  // Remove existing event listener to prevent duplicates
            element.addEventListener('click', handleAjaxLink);
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {
    initAjaxElements();
});

function handleAjaxLink(event) {
    const element = event.target.closest('a[data-ajax="true"]');
    if (!element || !confirmAction(element)) return;

    event.preventDefault();
    ajaxRequest(element, element.getAttribute('href'), element.getAttribute('data-ajax-method') || 'GET');
}

function handleAjaxForm(event) {
    const element = event.target.closest('form[data-ajax="true"]');
    if (!element || !confirmAction(element)) return;

    event.preventDefault();
    ajaxRequest(element, element.action, element.getAttribute('data-ajax-method') || element.method, new FormData(element));
}

function confirmAction(element) {
    const confirmMessage = element.getAttribute('data-ajax-confirm');
    return !confirmMessage || window.confirm(confirmMessage);
}

function ajaxRequest(element, url, method, body = null) {
    const updateTarget = document.querySelector(element.getAttribute('data-ajax-update'));
    const loadingElement = document.querySelector(element.getAttribute('data-ajax-loading'));
    const loadingDuration = parseInt(element.getAttribute('data-ajax-loading-duration')) || 0;
    const addToHistory = element.getAttribute('data-ajax-history') === 'false';
    const successHandlerName = element.getAttribute('data-ajax-success');
    const failureHandlerName = element.getAttribute('data-ajax-failure');

    if (loadingElement) {
        loadingElement.style.display = 'block';
        if (loadingDuration > 0) {
            setTimeout(() => loadingElement.style.display = 'none', loadingDuration);
        }
    }

    fetch(url, {
        method: method,
        body: method.toUpperCase() === 'GET' ? null : body,
        headers: {
            'X-Requested-With': 'XMLHttpRequest',
            'Content-Type': element.getAttribute('data-ajax-contenttype') || 'application/x-www-form-urlencoded; charset=UTF-8'
        }
    })
        .then(response => {
            if (!response.ok) throw response;
            return response.text();
        })
        .then(data => {
            if (updateTarget) {
                updateTarget.innerHTML = data;
            }
            triggerGlobalEvent('ajaxSuccess', element, data);
            if (addToHistory) {
                history.pushState({ path: url }, '', url);
            }
            hideLoading(loadingElement);
            invokeHandler(successHandlerName, data, element);
        })
        .catch(error => {
            console.error('Error:', error);
            handleError(element, error);
            triggerGlobalEvent('ajaxFailure', element, error);
            hideLoading(loadingElement);
            invokeHandler(failureHandlerName, error, element);
        });
}

function handleError(element, error) {
    const failureTarget = document.querySelector(element.getAttribute('data-ajax-failure'));
    if (failureTarget) {
        failureTarget.innerHTML = 'Request failed: ' + error.statusText || 'Error in request';
    }
}

function hideLoading(loadingElement) {
    if (loadingElement) {
        loadingElement.style.display = 'none';
    }
}

function triggerGlobalEvent(eventName, element, detail) {
    document.dispatchEvent(new CustomEvent(eventName, { detail: { element, response: detail } }));
}

function invokeHandler(handlerName, data, element) {
    if (handlerName && window[handlerName] && typeof window[handlerName] === 'function') {
        window[handlerName](data, element);
    }
}
