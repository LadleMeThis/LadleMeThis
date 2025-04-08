"use client"
import React, { useState, useEffect } from 'react';
import { fetchUpdateProfile, fetchProfile } from '@/scripts/user';

const ProfileUpdateForm = () => {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const [userId, setUserId] = useState('');

  const getProfileData = async () => {
    const data = await fetchProfile(userId)
    email = data.email
    username = data.username
  }

  useEffect(() => {
    getProfileData()

  }, [userId])
  

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (password !== confirmPassword) {
      setError('Passwords do not match!');
      return;
    }

    setError('');
    
    const updatedProfile = {
      username: username,
      email: email,
      newPassword: password,
    };

    console.log('Profile updated:', updatedProfile);
    fetchUpdateProfile(userId, updatedProfile)
  };

  const canSend = username || email || password;

  return (
    <div className="profile-update-form">
      <h2>Update Profile</h2>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="username">Username</label>
          <input
            type="text"
            id="username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
        </div>

        <div className="form-group">
          <label htmlFor="email">Email</label>
          <input
            type="email"
            id="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>

        <div className="form-group">
          <label htmlFor="password">Password</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>

        <div className="form-group">
          <label htmlFor="confirmPassword">Confirm Password</label>
          <input
            type="password"
            id="confirmPassword"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
          />
        </div>

        {error && <p className="error">{error}</p>}

        <button type="submit" className={`${canSend ? "" : "disabled"}`}>Update Profile</button>
      </form>
    </div>
  );
};

export default ProfileUpdateForm;
