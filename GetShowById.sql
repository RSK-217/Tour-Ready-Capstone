SELECT s.id, userId, groupId, g.groupName, venue, showDate, cityId, setList, showNotes, merchSales, payout, isFavorite
FROM [Show] s
JOIN [Group] g ON s.groupId = g.id
