package com.example.dpstuffproviderstore.fragment

import android.content.Context
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.other.ClientApiService
import kotlinx.android.synthetic.main.fragment_account_login.*
import kotlinx.android.synthetic.main.fragment_account_login.view.*
import kotlin.math.log

/**
 * Фрагмент формы авторизации пользователя
 */
class AccountLoginFragment : Fragment() {

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?,
                              savedInstanceState: Bundle?): View? {
        val mainActivity = activity as MainActivity
        val inflate = inflater.inflate(R.layout.fragment_account_login, container, false)

        inflate.btnLogin.setOnClickListener {
            ClientApiService().getClient(inputTextLogin.text.toString(), inputTextPassword.text.toString()){
                if(it != null){
                    mainActivity.client = it
                    val editor = mainActivity.getSharedPreferences("sp", Context.MODE_PRIVATE).edit()
                    editor.putBoolean("isLogin", true)
                    editor.putString("login", inputTextLogin.text.toString())
                    editor.putString("password", inputTextPassword.text.toString())
                    editor.apply()
                    mainActivity.accountFragment = AccountFragment()
                    mainActivity.makeCurrentFragment(AccountFragment())
                }
                else{
                    Toast.makeText(requireContext(), "Неправильный логин и/или пароль", Toast.LENGTH_LONG).show()
                }
            }
        }

        inflate.btnReg.setOnClickListener {
            mainActivity.makeCurrentFragment(RegistrationFragment())
        }

        return inflate
    }

}