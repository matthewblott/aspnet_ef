(function() {
  
  document.querySelectorAll('input,textarea,select').forEach((el) => el.addEventListener('invalid', (e) => e.preventDefault(), true));

  const selector = 'data-html5-compare';

  document.querySelectorAll(`input[${selector}]`).forEach((c) => {
    const p = document.getElementById(c.attributes[selector].value);
    const e = () => c.setCustomValidity(c.value === p.value ? '' : '.');

    p.addEventListener('change', e, true);
    c.addEventListener('keyup', e, true);

  });
  
})();