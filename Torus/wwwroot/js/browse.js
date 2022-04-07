
function populateBrowseCards(cont_id = "")
{
    // This function randomly selects placeholder 3D assets from a pre-defined list.
    // As Torus will later feature a backend for useful asset fetching, this function is intended for frontend demonstration purposes.
    jQuery.getJSON('/json/item_placeholders.json', function (data) {

        var randomPlaceholders = ["donut.png", "liminal_junk.png", "shader_ngloss.png", "congruent_cheesewedge.png"];
        var assetIndex = Math.floor(Math.random() * randomPlaceholders.length);

        const cardcont = document.getElementById(cont_id);
        var imageURL = "/img/items/" + randomPlaceholders[assetIndex];

        const item = data.items[assetIndex];
        var price = parseFloat(item.cost) / 100.0;
        var imageName = item.name + " (" + item.type + ")<br>$" + price.toFixed(2);
        var newhtml = "";

        newhtml += '<div class="card">';
        newhtml += `<a><img class="card-img-top" src="${imageURL}" width="10"></a>`;
        newhtml += `<hr><div class='card-body'>${imageName}</div></div>`;
        cardcont.innerHTML += newhtml;
    })


}


// NOTE TO SELF: cards will be structured like this:

        //<div class="card" style="width:350px">
        //    <img class="card-img-top" src="https://thiscatdoesnotexist.com" width="10">
        //    <div class="card-body">TorusTestThingy</div>
        //</div>