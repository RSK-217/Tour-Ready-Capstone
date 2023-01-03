import React from 'react';
import { signIn } from '../utils/auth';
import tourready from '../assets/tourready.png';

export default function LogIn() {
  return (
    <div className="text-center mt-5">
      <img src={tourready}></img>
      <button type="button" className="btn btn-success" onClick={signIn}>
        Sign In
      </button>
    </div>
  );
}
