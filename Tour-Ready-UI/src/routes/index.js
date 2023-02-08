// index for router
import React, { useState, useEffect } from 'react';
import { Route, Switch, useHistory } from 'react-router-dom';
import Authenticated from '../pages/Authenticated';
import EditGroup from '../Puts/EditGroup';
import Cities from '../pages/Cities';
import City from '../pages/City';
import AddCity from '../Posts/AddCity';
import EditCity from '../Puts/EditCity';
import Shows from '../pages/Shows';
import Show from '../pages/Show';
import EditShow from '../Puts/EditShow';
import AddShow from '../Posts/AddShow';

export default function Routes({ user }) {
  const [userExists, setUserExists] = useState({});
  const [currentUser, setCurrentUser] = useState({});

  const history = useHistory()

  useEffect(() => {
    fetch(`https://localhost:7108/api/User/CheckIfUserExists/${user.uid}`,
      {
        method: 'GET',
        headers: {
          'Access-Control-Allow-Origin': 'https://localhost:7108',
          'Content-Type': 'application/json'
        },
      },
    )
      .then((res) => res.json())
      .then((fbUser) => {
        fbUser === true ? setUserExists(fbUser) : RegisterUser()
      })
  }, [])

  useEffect(() => {
    GetUser(user)
  }, [])

  const GetUser = () => {
    fetch(`https://localhost:7108/api/User/GetUserByFirebaseId/${user.uid}`,
      {
        method: 'GET',
        headers: {
          'Access-Control-Allow-Origin': 'https://localhost:7108',
          'Content-Type': 'application/json'
        },
      },
    )
      .then((res) => res.json())
      .then((r) => {
        setCurrentUser(r)
      });
  }

  const RegisterUser = () => {
    const newUser = {
      name: user.displayName,
      email: user.email,
      firebaseId: user.uid,
      image: user.photoURL
    }
    const fetchOptions = {
      method: 'POST',
      headers: {
        'Access-Control-Allow-Origin': 'https://localhost:7108',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(newUser)
    }
    return fetch('https://localhost:7108/api/User/RegisterUser', fetchOptions)
      .then(res => res.json())
      .then(() => {
        history.push('/')
      })
  }

  return (
    <div>
      <Switch>
        <Route exact path="/" component={() => <Authenticated user={user} currentUser={currentUser} />} />
        <Route path="/group/edit/:groupId(\d+)" component={() => <EditGroup />} />

        <Route path="/shows" component={() => <Shows currentUser={currentUser} />} />
        <Route path="/show/:showId(\d+)" component={() => <Show />} />
        <Route path="/show/edit/:showId(\d+)" component={() => <EditShow currentUser={currentUser} />} />
        <Route path="/show/add" component={() => <AddShow currentUser={currentUser} />} />

        <Route exact path="/cities" component={() => <Cities currentUser={currentUser} />} />
        <Route path="/city/:cityId(\d+)" component={() => <City />} />
        <Route path="/city/add" component={() => <AddCity currentUser={currentUser} />} />
        <Route path="/city/edit/:cityId(\d+)" component={() => <EditCity />} />
        <Route path="*" component={() => <Authenticated user={user} currentUser={currentUser} />} />
      </Switch>
    </div>
  );
}
