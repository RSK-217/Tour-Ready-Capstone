export const getAllMembersByGroupId = (id) => {
    return fetch(`https://localhost:7108/api/GroupMember/GetAllMembersByGroupId/${id}`)
        .then(res => res.json())
}

export const getGroupById = (id) => {
    return fetch(`https://localhost:7108/api/Group/GetGroupById/${id}`)
        .then(res => res.json())
}