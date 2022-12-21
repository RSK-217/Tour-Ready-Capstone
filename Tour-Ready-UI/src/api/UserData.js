export const getAllCities = (currentUser) => {
    return fetch(`https://localhost:7108/api/City/GetAllCitiesByUserId/${currentUser.id}`)
        .then(res => res.json())
}

export const getAllShows = (currentUser) => {
    return fetch(`https://localhost:7108/api/Show/GetShowByUserId/${currentUser.id}`)
        .then(res => res.json())
}

export const getAllGroups = (currentUser) => {
    return fetch(`https://localhost:7108/api/Group/GetAllGroupsByUserId/${currentUser.id}`)
        .then(res => res.json())
}
