import React from "react";

export const ShowFilter = ({ groups, value, setFilter }) => {

    const handleChange = (e) => {
        const groupFilter = parseInt(e.target.value)
        setFilter(groupFilter)
    }
    return (
        <select value={value} onChange={handleChange}>
            <option value={0}>All shows</option>
            {groups.map((group) => {
                        return (
                            <option key={group.id} value={group.id}>{group.groupName}</option>
                        )
                    })}
        </select>
    )
}