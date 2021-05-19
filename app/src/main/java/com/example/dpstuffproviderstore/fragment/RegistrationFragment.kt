package com.example.dpstuffproviderstore.fragment

import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.models.ClientData
import com.example.dpstuffproviderstore.other.ClientApiService
import kotlinx.android.synthetic.main.fragment_registration.*
import kotlinx.android.synthetic.main.fragment_registration.view.*

class RegistrationFragment : Fragment() {

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val inflate = inflater.inflate(R.layout.fragment_registration, container, false)

        inflate.btnReg.setOnClickListener {
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
                    Log.i("myLog", "регистрация успешна, нихуя (а мб и нет)")
                }
                else{
                    Log.i("myLog", "пиздык чирик")
                }
            }
        }

        return inflate
    }

}