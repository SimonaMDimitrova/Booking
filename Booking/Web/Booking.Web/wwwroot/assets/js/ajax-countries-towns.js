let countries = document.getElementById("countriesId");
countries.value = 0;

countries.addEventListener("change", getTowns);

function getTowns() {
    let xhr = new XMLHttpRequest();
    let selectedCountryId = countries.value;
    let currency = document.getElementById("currencyId");

    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            let townsDropdown = document.getElementById('townsId');

            if (selectedCountryId != 0) {
                townsDropdown[0].innerText = 'Choose a town ...';
                while (townsDropdown.length > 1) {
                    townsDropdown.remove(1);
                }

                var towns = JSON.parse(this.responseText);
                for (let i = 0; i < towns.length; i++) {
                    let town = towns[i];
                    let townsDropdown = document.getElementById('townsId');
                    let option = document.createElement("option");
                    option.text = town.value;
                    option.value = town.key;
                    townsDropdown.add(option);
                }
                getCurrencyCode(currency, selectedCountryId);
            } else {
                while (townsDropdown.length > 1) {
                    townsDropdown.remove(1);
                }

                townsDropdown[0].innerText = 'Choose a country first ...';
                currency.innerText = 'Budget';
            }
        }
    };
    xhr.open("GET", `/Home/GetTowns/${selectedCountryId}`);
    xhr.send();
}

function getCurrencyCode(currency, countryId) {
    let xhr = new XMLHttpRequest();

    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            currency.innerText = `Budget - ${this.responseText}`;
        }
    }
    xhr.open("GET", `/Home/GetCurrencyCode/${countryId.toString()}`);
    xhr.send();
}