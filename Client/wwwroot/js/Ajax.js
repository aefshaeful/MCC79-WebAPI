$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon"
}).done((result) => {
    let temp = "";
    $.each(result.results, (key, val) => {
        temp += `<tr>
                    <td>${key + 1}</td>
                    <td>${val.name}</td>
                    <td><button onclick="detail('${val.url}')" data-bs-toggle="modal" data-bs-target="#modalPokemon" class="btn btn-primary">Detail</button></td>
                </tr>`;
    })
    $("#tbodySW").html(temp);
});


function detail(stringURL) {
    $.ajax({
        url: stringURL
    }).done(res => {
        let temp = "";
        $.each(res.types, (key, val) => {
            temp += `<div class="badge bg-warning text-dark">${val.type.name}</div> `;
        });
        $("#exampleModalLabel").html(res.name);
        $("#title").html(res.name);
        $("#imagePokemon").attr("src", res.sprites.other.home.front_default);
        $("#hp").css("width", res.stats[0].base_stat + "%").html("HP : " + res.stats[0].base_stat);
        $("#attack").css("width", res.stats[1].base_stat + "%").html("Attack : " + res.stats[1].base_stat);
        $("#defense").css("width", res.stats[2].base_stat + "%").html("Defense : " + res.stats[2].base_stat);
        $("#sattack").css("width", res.stats[3].base_stat + "%").html("Spesial Attack : " + res.stats[3].base_stat);
        $("#sdefense").css("width", res.stats[4].base_stat + "%").html("Spesial Defense : " + res.stats[4].base_stat);
        $("#speed").css("width", res.stats[5].base_stat + "%").html("Speed : " + res.stats[5].base_stat);
        $("#height").html(res.height);
        $("#weight").html(res.weight);
        $("#experience").html(res.base_experience);
        $("#types").html(temp);

        console.log(res);
    })
};