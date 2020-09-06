<template>
  <div>
    <v-sheet>
      <h1>New Transaction</h1>
      <v-row>
        <v-col lg="3" md="3" sm="12" cols="12" class="align-self-start">
          <h2>Account ID</h2>
        </v-col>
        <v-col lg="9" md="9" sm="12" cols="12" class="align-self-center">
          <v-select
            label="Account ID"
            :items="accountList"
            v-model="transactionData.AccountId"
            filled
            outlined
          ></v-select>
        </v-col>
        <v-col lg="3" md="3" sm="12" cols="12" class="align-self-start">
          <h2>Transfer Type</h2>
        </v-col>
        <v-col lg="9" md="9" sm="12" cols="12" class="align-self-center">
          <v-select
            label="Type"
            :items="transferTypes"
            v-model="transterTypeSelection"
            filled
            outlined
          ></v-select>
        </v-col>
        <v-col lg="3" md="3" sm="12" cols="12" class="align-self-start">
          <h2>Amount</h2>
        </v-col>
        <v-col lg="9" md="9" sm="12" cols="12" class="align-self-center">
          <v-text-field
            label="Amount"
            v-model="transactionData.amount"
            type="number"
            min="0"
            single-line
            outlined
          ></v-text-field>
        </v-col>
          <!-- <v-col lg="3" md="3" sm="12" cols="12" class="align-self-start">
          <h2>Date</h2>
        </v-col>
        <v-col lg="9" md="9" sm="12" cols="12" class="align-self-center">
         <v-menu
            ref="menu1"
            v-model="menu1"
            :close-on-content-click="false"
            transition="scale-transition"
            offset-y
            max-width="290px"
            min-width="290px"
          >
            <template v-slot:activator="{ on, attrs }">
              <v-text-field
                v-model="dateFormatted"
                label="Date"
                hint="MM/DD/YYYY format"
                persistent-hint
                v-bind="attrs"
                @blur="date = parseDate(dateFormatted)"
                v-on="on"
              ></v-text-field>
            </template>
            <v-date-picker v-model="date" no-title @input="menu1 = false"></v-date-picker>
          </v-menu>
        </v-col> -->
        <v-col lg="3" md="3" sm="12" cols="12" class="align-self-start">
          <h2>Type</h2>
        </v-col>
        <v-col lg="9" md="9" sm="12" cols="12" class="align-self-center">
          <v-select
            label="Type"
            :items="ExpenseType"
            v-model="transactionData.Type"
            filled
            outlined
          ></v-select>
        </v-col>
        <v-col lg="3" md="3" sm="12" cols="12" class="align-self-start">
          <h2>Description</h2>
        </v-col>
        <v-col lg="9" md="9" sm="12" cols="12" class="align-self-center">
          <v-text-field label="Description" v-model="transactionData.Desc" single-line outlined></v-text-field>
        </v-col>
        <v-col cols="6" class="align-self-center">
          <v-btn color="primary" dark @click="submitForm">Submit</v-btn>
        </v-col>
        <v-col cols="6" class="align-self-center">
          <v-btn color="warning" dark>Clear</v-btn>
        </v-col>
      </v-row>
    </v-sheet>
  </div>
</template>

<script>
import AccountService from '@/services/account.service'
import TransactionService from '@/services/transaction.service'
export default {
  name: 'TransactionForm',
  data () {
    return {
      account: null,
      transferTypes: ['Debit', 'Credit'],
      transterTypeSelection: null,
      ExpenseType: ['Food', 'Fuel', 'Toll', 'Entertainment', 'Health', 'Utilities', 'Transport', 'Maintanence', 'Transfer', 'Donation', 'Salary', 'Education'],
      accountList: [],
      transactionData: {
        Id: '',
        AccountId: '',
        TransDate: new Date(),
        amount: '',
        TransType: null,
        Type: '',
        Desc: ''
      },
      currentBalance: 0.0,
      newBalance: 0.0,
      canCalculate: false
    }
  },
  mounted () {
    AccountService.getAll()
      .then(response => {
        this.account = response.data

        this.account.forEach(e => {
          this.accountList.push(e.id)
        })
      })
      .catch(error => {
        console.log(error)
        this.errored = true
      })
      .finally(() => {
        this.loading = false
      })
  },
  methods: {
    submitForm () {
      var today = new Date()
      var dd = String(today.getDate()).padStart(2, '0')
      var mm = String(today.getMonth() + 1).padStart(2, '0')
      var yyyy = today.getFullYear()
      today = yyyy + '-' + mm + '-' + dd
      var iddate = dd + mm + yyyy + String(Math.floor(Math.random() * 101))
      this.transactionData.Id = iddate
      this.transactionData.TransDate = today
      this.transactionData.amount = parseFloat(this.transactionData.amount)
      if (this.transterTypeSelection === 'Debit') {
        this.transactionData.TransType = 1
      } else {
        this.transactionData.TransType = 2
      }
      TransactionService.postNewTransaction(this.transactionData)
        .then(this.$alert('New Transaction Added!', 'Success', 'success'))
        .catch(error => {
          console.log(error)
          this.$alert('Something happened!', 'Warning', 'warning')
        })
        .finally(() => {
          this.canCalculate = true
          this.UpdateData(this.canCalculate)
        })
    },
    UpdateData (pcanCalculate) {
      if (pcanCalculate === true) {
        AccountService.getAccount(this.transactionData.AccountId)
          .then(response => {
            const accountData = response.data
            this.currentBalance = accountData.balance
            if (this.transactionData.TransType === 1) {
              this.newBalance = this.currentBalance - this.transactionData.amount
            }
            if (this.transactionData.TransType === 2) {
              this.newBalance = this.currentBalance + this.transactionData.amount
            }
            this.PutData()
          })
      }
    },
    PutData () {
      AccountService.putUpdate(this.transactionData.AccountId, {
        id: this.transactionData.AccountId,
        balance: this.newBalance,
        latestDate: this.transactionData.TransDate
      })
    }

  }
}
</script>

<style scoped>
.v-sheet {
  margin-left: 20px;
  margin-right: 20px;
}
</style>
