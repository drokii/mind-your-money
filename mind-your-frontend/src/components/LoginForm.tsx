'use client';
import React from 'react';
import { useFormik } from 'formik';
import { logIn } from '@/service/AuthService';

export const LoginForm = () => {

    const formik = useFormik({
        initialValues: {
            name: '',
            password: '',
        },
        onSubmit: values => {
            logIn(values.name, values.password)
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
            <label htmlFor="password">Password</label>
            <input
                id="password"
                name="password"
                type="password"
                onChange={formik.handleChange}
                value={formik.values.password}
            />
            <button type="submit">Submit</button>
        </form>
    );

};