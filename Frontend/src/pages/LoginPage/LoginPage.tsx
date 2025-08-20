import React from "react";
import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useAuth } from "../../Context/useAuth";
import { useForm } from "react-hook-form";
import { Link } from "react-router";

type Props = {};

type LoginFormsInputs = {
  userName: string;
  password: string;
};

const validation = Yup.object().shape({
  userName: Yup.string().required("Username is required"),
  password: Yup.string().required("Password is required"),
});

const LoginPage = (props: Props) => {
  const { loginUser } = useAuth();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormsInputs>({ resolver: yupResolver(validation) });

  const handleLogin = (form: LoginFormsInputs) => {
    loginUser(form.userName, form.password);
  };
  return (
    <section className="bg-gray-100 min-h-screen flex items-center justify-center">
  <div className="w-full bg-white rounded-lg shadow-lg sm:max-w-md p-8">
    <h1 className="text-2xl font-bold text-gray-900 mb-6 text-center">
      Sign in to your account
    </h1>
    <form className="space-y-5" onSubmit={handleSubmit(handleLogin)}>
      {/* Username */}
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
          placeholder="Enter your username"
          className="w-full border border-gray-300 rounded-lg p-2.5 text-gray-900 focus:ring-lightGreen focus:border-lightGreen"
          {...register("userName")}
        />
        {errors.userName && (
          <p className="text-red-500 text-sm mt-1">
            {errors.userName.message}
          </p>
        )}
      </div>

      {/* Password */}
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
          className="w-full border border-gray-300 rounded-lg p-2.5 text-gray-900 focus:ring-lightGreen focus:border-lightGreen"
          {...register("password")}
        />
        {errors.password && (
          <p className="text-red-500 text-sm mt-1">
            {errors.password.message}
          </p>
        )}
      </div>

      {/* Submit */}
      <button
        type="submit"
        className="w-full text-white bg-lightGreen hover:opacity-80 focus:ring-4 focus:ring-lightGreen/50 font-medium rounded-lg text-sm px-5 py-2.5"
      >
        Sign in
      </button>

      {/* Signup link */}
      <p className="text-sm font-light text-gray-500 text-center">
        Don't have an account yet?{" "}
        <Link
          to="/register"
          className="font-medium text-lightGreen hover:underline"
        >
          Sign up
        </Link>
      </p>
    </form>
  </div>
</section>
  );
};

export default LoginPage;