<template>
  <v-container>
    <v-layout justify-center v-if="!showProgressBar">
      <v-flex xs12 sm6 md5>
        <v-card>
          <v-toolbar dark color="teal lighten-1" >
            <v-toolbar-title>Drogueria Colombia's Manager</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <v-form ref="form">
              <v-text-field
              prepend-icon="person"
              v-model="email"              
              label="Email"                  
              >
              </v-text-field>

              <v-text-field
              prepend-icon="lock"
              v-model="password"              
              type="password"
              label="Password"              
              >
              </v-text-field>   
            </v-form>  
            <div class="text-xs-center">
              <v-btn align-center dark color="teal lighten-1"  @click="submit()"> Log in</v-btn> 
              <v-btn align-center dark color="teal lighten-1"  @click=""> Sign in</v-btn>                   
            </div>                    
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>

     <v-layout justify-center v-if="showProgressBar">
      <ProgressBar/>
    </v-layout>
   
  </v-container>
</template>

<script>
  import axios from 'axios'
  import ProgressBar from '@/components/ProgressBar'
  
  export default {
    name:'userCredentialsLogin',
    data() {
      return {
        showProgressBar:false,
        email: '',
        password:'',
      }
    },
    methods: {
      submit() {
        this.showProgressBar = true
        let me = this;       
        axios.post('https://localhost:44397/api/login',{											
					Email:me.email,
					Password: me.password					
        })
          .then(response =>{     
            console.log(response)       												
            return response.data 
        })
          .then(data =>{            
            this.$store.dispatch('saveToken', data.token)
            this.$router.push({name:'home'})        
          })
          .catch(function(err){
            console.log(err)
          })
      }         
    }
    
  }
</script>

