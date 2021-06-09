package com.example.dpstuffprovider.fragments

import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.inputmethod.EditorInfo
import android.widget.Toast
import androidx.fragment.app.DialogFragment
import com.example.dpstuffprovider.MoreInfo
import com.example.dpstuffprovider.R
import com.example.dpstuffprovider.api.ApiService
import kotlinx.android.synthetic.main.fragment_dialog_finish_order.view.*

class DialogFinishOrderFragment : DialogFragment() {

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val inflate = inflater.inflate(R.layout.fragment_dialog_finish_order, container, false)
        val parentActivity = activity as MoreInfo

        inflate.inputTextCode.setOnEditorActionListener { v, actionId, event ->
            if(actionId == EditorInfo.IME_ACTION_DONE){
                if(inflate.inputTextCode.text.toString() == parentActivity.orderInfo!!.codeToFinish){
                    Log.i("myLog", "Код верен")

                    parentActivity.orderInfo!!.status = "Завершен"
                    ApiService().updateOrderStatus(parentActivity.orderInfo!!){
                        if(it != null){
                            Toast.makeText(requireContext(), "Заказ завершен", Toast.LENGTH_LONG).show()
                            this@DialogFinishOrderFragment.dismiss()
                            parentActivity.finish()
                        }
                        else{
                            Toast.makeText(requireContext(), "Неизвестная ошибка, повторите позже", Toast.LENGTH_LONG).show()
                        }
                    }
                }
                else{
                    Log.i("myLog", "Код не верен")
                    Toast.makeText(requireContext(), "Неверный код", Toast.LENGTH_LONG).show()
                }

                true
            } else {
                false
            }
        }

        return inflate
    }

}