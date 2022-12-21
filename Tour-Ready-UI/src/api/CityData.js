export const getCityById = (id) => {
    return fetch(`https://localhost:7108/api/City/GetCityById/${id}`)
        .then(res => res.json())
}