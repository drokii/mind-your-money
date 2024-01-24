'use client';
import React from 'react';
import { useFormik } from 'formik';
import { register } from '@/service/AuthService';

export const RegisterForm = () => {

    const formik = useFormik({
        initialValues: {
            name: '',
            email: '',
            password: '',
        },
        onSubmit: values => {
            register(values.name, values.password, values.email)
        },

    });
    return (
        <form onSubmit={formik.handleSubmit}>
            <label htmlFor="name">name</label>
            <input
                id="name"
                name="name"
                type="text"
                onChange={formik.handleChange}
                value={formik.values.name}
            />
            <label htmlFor="email">Password</label>
            <input
                id="password"
                name="password"
                type="password"
                onChange={formik.handleChange}
                value={formik.values.password}
            />
            <label htmlFor="email">Email Address</label>
            <input
                id="email"
                name="email"
                type="email"
                onChange={formik.handleChange}
                value={formik.values.email}
            />
            <button type="submit">Submit</button>
        </form>
    );

};