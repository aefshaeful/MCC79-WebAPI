//array of object
let arrayMhsObj = [
    { nama: "budi", nim: "a112015", umur: 20, isActive: true, fakultas: { name: "komputer" } },
    { nama: "joko", nim: "a112035", umur: 22, isActive: false, fakultas: { name: "ekonomi" } },
    { nama: "herul", nim: "a112020", umur: 21, isActive: true, fakultas: { name: "komputer" } },
    { nama: "herul", nim: "a112032", umur: 25, isActive: true, fakultas: { name: "ekonomi" } },
    { nama: "herul", nim: "a112040", umur: 21, isActive: true, fakultas: { name: "komputer" } },
]

let fakultasKomputer = [];

for (let i = 0; i < arrayMhsObj.length; i++)
{
    let mahasiswa = arrayMhsObj[i];
    if (mahasiswa.fakultas.name == "komputer")
    {
        fakultasKomputer.push(mahasiswa);
    }

    let nimLast = parseInt(mahasiswa.nim.slice(-2));
    if (nimLast >= 30)
    {
        mahasiswa.isActive = false;
    }
}

console.log(fakultasKomputer);
console.log(arrayMhsObj);
