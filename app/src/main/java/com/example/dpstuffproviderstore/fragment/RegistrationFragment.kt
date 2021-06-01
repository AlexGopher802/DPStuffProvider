package com.example.dpstuffproviderstore.fragment

import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.models.ClientData
import com.example.dpstuffproviderstore.other.ClientApiService
import kotlinx.android.synthetic.main.fragment_registration.*
import kotlinx.android.synthetic.main.fragment_registration.view.*

/**
 * Фрагмент регистрации нового пользователя
 */
class RegistrationFragment : Fragment() {

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val inflate = inflater.inflate(R.layout.fragment_registration, container, false)
        val mainActivity = activity as MainActivity

        inflate.btnReg.setOnClickListener {
            if(!validForm()){
                return@setOnClickListener Unit
            }
            val client = ClientData(
                    id = null,
                    lastName = inputTextLastName.text.toString(),
                    firstName = inputTextFirstName.text.toString(),
                    patronymic = inputTextPatronymic.text.toString(),
                    email = inputTextEmail.text.toString(),
                    phone = inputTextPhone.text.toString(),
                    login = inputTextLogin.text.toString(),
                    password = null
            )

            ClientApiService().addClient(client, inputTextPassword.text.toString()){
                if(it != null){
                    Toast.makeText(context!!, "Регистрация успешна", Toast.LENGTH_LONG).show()
                    mainActivity.makeCurrentFragment(AccountNotLoginFragment())
                }
                else{
                    Toast.makeText(context!!, "Что-то пошло не по плану, повторите позже", Toast.LENGTH_LONG).show()
                }
            }
        }

        return inflate
    }

    fun validForm() : Boolean{
        if(inputTextFirstName.text.isNullOrEmpty() ||
                inputTextLastName.text.isNullOrEmpty() ||
                inputTextPatronymic.text.isNullOrEmpty() ||
                inputTextEmail.text.isNullOrEmpty() ||
                inputTextPhone.text.isNullOrEmpty() ||
                inputTextLogin.text.isNullOrEmpty() ||
                inputTextPassword.text.isNullOrEmpty()){
            Toast.makeText(context!!, "Заполните все поля", Toast.LENGTH_LONG).show()
            return false
        }

        if(!inputTextEmail.text!!.contains('@') || !inputTextEmail.text!!.contains('.')){
            Toast.makeText(context!!, "Некорректный адрес почты", Toast.LENGTH_LONG).show()
            return false
        }

        if(inputTextPassword.text!!.length < 8){
            Toast.makeText(context!!, "Пароль должени быть не короче 8-ми символов", Toast.LENGTH_LONG).show()
            return false
        }

        if(inputTextPassword.text.toString() != inputTextRepeatPassword.text.toString()){
            Toast.makeText(context!!, "Пароли не совпадают", Toast.LENGTH_LONG).show()
            return false
        }

        return true
    }
}