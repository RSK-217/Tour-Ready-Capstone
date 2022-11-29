SELECT gm.id, userId, groupId, isEditor, g.groupName, g.[image], u.[name]
FROM [GroupMember] gm
JOIN [Group] g ON gm.groupId = g.id
JOIN [User] u ON gm.userId = u.id

