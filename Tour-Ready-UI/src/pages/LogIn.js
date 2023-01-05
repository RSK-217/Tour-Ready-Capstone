import React from 'react';
import { signIn } from '../utils/auth';

import "../styles/login.css";

export default function LogIn() {
  return (
    <body className='login-background'>
    <div className='login-content'>
    <div className="text-center mt-5">
      <h1 className='login-title'>tour <i>ready</i></h1>
      <button type="button" className="login-btn" onClick={signIn}>
        Sign in
      </button>
    </div>
    </div>
    </body>
  );
}
