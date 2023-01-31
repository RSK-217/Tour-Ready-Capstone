import React from 'react'
import { MdCancel, MdCheck, MdSave } from 'react-icons/md'

export default function AddPeople({setClickPerson}) {

  const Cancel = () => {
    setClickPerson(null)
  }

  return (
    <>
    <div>AddPeople</div>
    <MdCheck></MdCheck>
    <MdCancel onClick={Cancel}></MdCancel>
    </>
  )
}

