<template>
    <v-card>
      <v-card-title>
        <span class="headline">Editar Usuario</span>
      </v-card-title>
      <v-card-text>
        <v-form>      
          <v-container>
            <v-row>
              <v-col cols="12" sm="6" md="5">
                <v-select
                  v-model="userObj.idRole"
                  :items="roles"
                  item-text="roleName"
                  item-value="idRole"
                  label="Rol*"
                  required
                ></v-select>                         
              </v-col>
              <v-col cols="12" sm="6" md="12">
                <v-text-field label="Nombre de Usuario*" v-model="userObj.userName" required></v-text-field>
              </v-col>                                    
              
              <v-col cols="12">
                <v-text-field label="Tipo de documento" v-model="userObj.documentType"></v-text-field>
              </v-col>                     

                <v-col cols="12">
                <v-text-field label="Numero de documento" v-model="userObj.documentNumber" ></v-text-field>
              </v-col>

              <v-col cols="12" md="12">
                <v-text-field label="Direccion" v-model="userObj.address" required></v-text-field>
              </v-col>  

              <v-col cols="12">
                <v-text-field label="Numero de telefono" v-model="userObj.phoneNumber" required></v-text-field>
              </v-col>

              <v-col cols="12" md="12">
                <v-text-field label="Email*" v-model="userObj.email" required></v-text-field>
              </v-col>  

              <v-col cols="12" md="12">
                <v-text-field 
                  label="Contraseña*" 
                 
                  :append-icon="show1 ? 'visibility' : 'visibility_off'"
                  :type="show1 ? 'text':'password'"
                  @click:append="show1 = !show1"                   
                  required>
                  </v-text-field>
              </v-col>  
              
            </v-row>
          </v-container>
          <small>*indica campo obligatorio</small>        
        </v-form>
      </v-card-text>
      <v-card-actions>
        <div class="flex-grow-1"></div>
        <v-btn color="blue darken-1" text @click="cancel()">Cerrar</v-btn>
        <v-btn color="blue darken-1" text @click="save()">Guardar</v-btn>
      </v-card-actions>
    </v-card>
  
</template>

<script>
  export default {
    props: {     
      user:{
        type:Object,
        required:true
      },
      roles:{
        type:Array,
        required:true
      },   
    },
    data() {
      return {
        userObj: Object.assign({}, this.user),
        show1: false
      }
    },
    watch: {
     //este watcher nos ayuda a actualizar 
     //el user enviado desde el componente padre
      user(newValue, oldValue) {        
       this.userObj = Object.assign({}, newValue)
     }
   }, 
    methods: {
      save() {
        this.$emit('save',{user:this.userObj})
      },
      cancel(){
        this.$emit('cancel',false)
      }
    },
  }
</script>
