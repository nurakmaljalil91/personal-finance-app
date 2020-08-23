<template>
  <div>
    <v-sheet>
      <h1>New Account</h1>
      <v-row>
        <v-col lg="3"  md="3" sm="12" cols="12" class="align-self-start"><h2>Account ID</h2></v-col>
        <v-col lg="9"  md="9" sm="12" cols="12" class="align-self-center">
          <v-text-field label="Account ID" single-line outlined></v-text-field>
        </v-col>
        <v-col lg="3"  md="3" sm="12" cols="12" class="align-self-start"><h2>Balance</h2></v-col>
        <v-col lg="9"  md="9" sm="12" cols="12" class="align-self-center">
          <v-text-field label="Balance" single-line outlined></v-text-field>
        </v-col>
        <v-col cols="6"  class="align-self-center"> <v-btn color="primary" dark>Submit</v-btn></v-col>
        <v-col cols="6"  class="align-self-center"> <v-btn color="warning" dark>Clear</v-btn></v-col>
      </v-row>
    </v-sheet>
  </div>
</template>

<script>
export default {
  name: 'NewAccount',
  data () {
    return {
      newAccountData: {
        Id: '',
        Balance: '',
        LatestDate: new Date()
      }
    }
  }
}
</script>

<style scoped>
.v-sheet{
    margin-left: 20px;
    margin-right: 20px;
}
</style>
