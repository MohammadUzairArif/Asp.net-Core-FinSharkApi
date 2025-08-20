import React from "react";
import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useAuth } from "../../Context/useAuth";
import { useForm } from "react-hook-form";
import { Link } from "react-router";

type Props = {};

type RegisterFormsInputs = {
  email: string;
  userName: string;
  password: string;
};

const validation = Yup.object().shape({
  email: Yup.string().email("Invalid email").required("Email is required"),
  userName: Yup.string().required("Username is required"),
  password: Yup.string().required("Password is required"),
});

const RegisterPage = (props: Props) => {
  const { registerUser } = useAuth();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterFormsInputs>({ resolver: yupResolver(validation) });

  const handleRegister = (form: RegisterFormsInputs) => {
    registerUser(form.email, form.userName, form.password);
  };
  return (
    <section className="bg-gray-100 min-h-screen flex items-center justify-center">
      <div className="w-full bg-white rounded-lg shadow-lg sm:max-w-md p-8">
        <h1 className="text-2xl font-bold text-gray-900 text-center mb-6">
          Create your account
        </h1>
        <form className="space-y-4" onSubmit={handleSubmit(handleRegister)}>
          <div>
            <label
              htmlFor="email"
              className="block mb-2 text-sm font-medium text-gray-700"
                >
                   Email
            </label>
            <input
              type="text"
              id="email"
              className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-lightGreen focus:border-lightGreen block w-full p-2.5"
              placeholder="Enter your email"
              {...register("email")}
            />
            {errors.email && (
              <p className="text-red-500 text-sm">{errors.email.message}</p>
            )}
              </div>
              <div>
            <label
              htmlFor="username"
              className="block mb-2 text-sm font-medium text-gray-700"
            >
              Username
            </label>
            <input
              type="text"
              id="username"
              className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-lightGreen focus:border-lightGreen block w-full p-2.5"
              placeholder="Enter your username"
              {...register("userName")}
            />
            {errors.userName && (
              <p className="text-red-500 text-sm">{errors.userName.message}</p>
            )}
          </div>
          <div>
            <label
              htmlFor="password"
              className="block mb-2 text-sm font-medium text-gray-700"
            >
              Password
            </label>
            <input
              type="password"
              id="password"
              placeholder="••••••••"
              className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-lightGreen focus:border-lightGreen block w-full p-2.5"
              {...register("password")}
            />
            {errors.password && (
              <p className="text-red-500 text-sm">{errors.password.message}</p>
            )}
          </div>

          <button
            type="submit"
            className="w-full text-white bg-lightGreen hover:opacity-80 focus:ring-4 focus:ring-lightGreen/50 font-medium rounded-lg text-sm px-5 py-2.5"
          >
            Sign up
          </button>

          <p className="text-sm font-light text-gray-500 text-center">
            Already have an account?{" "}
            <Link
              to="/login"
              className="font-medium text-lightGreen hover:underline"
            >
              Sign in
            </Link>
          </p>
        </form>
               </div>
    </section>
  );
};

export default RegisterPage;